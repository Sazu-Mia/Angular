import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DomSanitizer } from '@angular/platform-browser';
import { Candidate } from '../../../models/data/candidate';
import { CandidateViewModel } from '../../../models/view-model/candidate-view-model';
import { CandidateService } from '../../../services/data/candidate.service';

@Component({
  selector: 'app-candidate-view',
  templateUrl: './candidate-view.component.html',
  styleUrls: ['./candidate-view.component.css']
})
export class CandidateViewComponent implements OnInit {
  picturePath = 'http://localhost:5273/Pictures'
  candidates: CandidateViewModel[] = [];
  columns = ['picture', 'candidateName', 'birthDate', 'appliedFor',  'expectedSalary', 'workFromHome', 'qualifactionCount','details', 'actions'];
  dataSource: MatTableDataSource<CandidateViewModel> = new MatTableDataSource(this.candidates);
  @ViewChild(MatSort, { static: false }) sort!: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;
  constructor(
    private candidateSvc: CandidateService,
    private sanitizer: DomSanitizer
  ) { }
  getSafeUrl(p: string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.picturePath + "/" + p);
  }
  confirmDelete(item: CandidateViewModel) {
    if (confirm('Are you sure to delete?')) {
      this.candidateSvc.delete(<number>item.candidateId)
        .subscribe({
          next: r => {
            this.dataSource.data = this.dataSource.data.filter(x => x.candidateId != item.candidateId);
          },
          error: err => {
            console.log(err.message || err);
          }
        })
    }
  }
  ngOnInit(): void {
    this.candidateSvc.getVM()
      .subscribe({

        next: r => {
          this.candidates = r;
          this.dataSource.data = this.candidates;
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
        },
        error: err => {
          console.log(err.message || err);
        }
      });
    }

}
