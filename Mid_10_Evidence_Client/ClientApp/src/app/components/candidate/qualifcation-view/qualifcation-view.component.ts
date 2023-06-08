import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { Qualification } from '../../../models/data/qualification';
import { CandidateService } from '../../../services/data/candidate.service';

@Component({
  selector: 'app-qualifcation-view',
  templateUrl: './qualifcation-view.component.html',
  styleUrls: ['./qualifcation-view.component.css']
})
export class QualifcationViewComponent implements OnInit {
 
  qualifcations: Qualification[] = [];
  dataSource: MatTableDataSource<Qualification> = new MatTableDataSource(this.qualifcations);
  columns = ['degree', 'institute', 'passingYear', 'result'];
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  constructor(private candidateSvc: CandidateService,
    private actvatedRoute: ActivatedRoute
  ) { }
  ngOnInit(): void {
    let id: number = this.actvatedRoute.snapshot.params['id'];
    this.candidateSvc.getQulifications(id)
      .subscribe({
        next: r => {
          this.qualifcations = r;
          this.dataSource.data = this.qualifcations;
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
          console.log(this.qualifcations);
        },
        error: err => {
          console.log(err.message || err);
        }
      })
  }
}
