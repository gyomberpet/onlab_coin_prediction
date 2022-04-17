import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-simulation-edit',
  templateUrl: './simulation-edit.component.html',
  styleUrls: ['./simulation-edit.component.css']
})
export class SimulationEditComponent implements OnInit {

  form: FormGroup;
  description: string;

  constructor(
    public dialogRef: MatDialogRef<SimulationEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) { }

  ngOnInit(): void {
  }
    
  onNoClick(): void {
    this.dialogRef.close();
  }

}
