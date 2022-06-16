import pandas as pd
import numpy as np
import datetime
from sklearn.model_selection import train_test_split
from keras.models import Sequential
from keras.layers import Dense, LSTM
import matplotlib.pyplot as plt
import matplotlib.dates as mdates
from sklearn.preprocessing import MinMaxScaler
from sklearn.metrics import mean_squared_error
import sys
import json

start_date_input = sys.argv[1]
end_date_input = sys.argv[2]
interval = sys.argv[3]
train_size_percentage = float(sys.argv[4]) / 100
train_finished_successfully = sys.argv[5]

df = pd.read_csv('C:\\Programming\onlab\data\Binance_BTCUSDT_{}.csv'.format(interval))

df = df[['Date', 'Open', 'High', 'Low', 'Close']]
df['Date'] = pd.to_datetime(df['Date'])

def filter_df(df, start_date, end_date):
    return df[df['Date'].between(start_date, end_date)]

start = pd.to_datetime(start_date_input)
end = pd.to_datetime(end_date_input)
sliding_window_size = 60

df_filtered = filter_df(df, start, end)

scaler = MinMaxScaler(feature_range=(0,1))
close_prices = df_filtered.filter(['Close']).values

scaled_dataset = scaler.fit_transform(close_prices)

train_array, test_array = train_test_split(scaled_dataset, train_size = train_size_percentage, random_state=42, shuffle=False)

def create_df(array, number_of_columns):
    x_train = []
    y_train = []
    for i in range(number_of_columns, len(array)):
        x_train.append(array[i-number_of_columns:i, 0])
        y_train.append(array[i, 0])
    return x_train, y_train

x_train, y_train = create_df(train_array, sliding_window_size)

y_train = np.array(y_train)

x_train = np.array(x_train)
x_train = np.reshape(x_train, (x_train.shape[0], x_train.shape[1], 1))

model = Sequential()
model.add(LSTM(50, return_sequences=True, input_shape=(x_train.shape[1],1)))
model.add(LSTM(50, return_sequences=False))
model.add(Dense(25))
model.add(Dense(1))


model.compile(optimizer='adam', loss='mean_squared_error')

model.fit(x_train, y_train, batch_size=1, epochs=1)

print(train_finished_successfully)

expended_test_array = scaled_dataset[len(train_array)-60:]
x_test, _ = create_df(expended_test_array, sliding_window_size)

train_length = len(train_array)
df_close_prices = df_filtered['Close']
y_true = df_close_prices[train_length:]

x_test = np.array(x_test)
x_test = np.reshape(x_test, (x_test.shape[0], x_test.shape[1], 1))

predictions = model.predict(x_test)
y_pred = scaler.inverse_transform(predictions)
y_pred = np.array(y_pred, dtype='float')


time_predict_depth = 15

predictions_per_current_depth = []
current_x_test = x_test

for i in range(0, time_predict_depth):
    preds = model.predict(current_x_test)
    current_y_pred = scaler.inverse_transform(preds)
    current_y_pred = np.array(current_y_pred, dtype='float')
    
    predictions_per_current_depth.append(current_y_pred)
    
    current_x_test_array = np.array(current_x_test)
    create_df_from = current_x_test_array.reshape(current_x_test_array.shape[0], current_x_test_array.shape[1])
    
    current_x_test_df = pd.DataFrame(create_df_from)
    current_x_test_df = current_x_test_df.drop(current_x_test_df.columns[0], axis=1)
    new_column_name = "depth_" + str(i)
    current_x_test_df[new_column_name] = preds
    new_array = np.array(current_x_test_df)
    
    current_x_test = np.reshape(new_array, (new_array.shape[0], new_array.shape[1], 1))

pred_result_array = np.array(predictions_per_current_depth)
shapedPredictions = np.reshape(pred_result_array, (
    pred_result_array.shape[0], 
    pred_result_array.shape[1]))

valid_result_array = np.array(y_true)
shapedValids = np.reshape(valid_result_array, (valid_result_array.shape[0]))

result = {"predictions": shapedPredictions.tolist(), "valids": shapedValids.tolist()}

print(json.dumps(result))



