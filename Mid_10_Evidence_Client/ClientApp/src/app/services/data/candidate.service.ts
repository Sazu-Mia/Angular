import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Candidate } from '../../models/data/candidate';
import { Qualification } from '../../models/data/qualification';
import { CandidateInputModel } from '../../models/view-model/candidate-input-model';
import { ImagePathRespoonse } from '../../models/view-model/image-path-respoonse';

@Injectable({
  providedIn: 'root'
})
export class CandidateService {

  constructor(
    private http: HttpClient
  ) { }
  get(): Observable<Candidate[]> {
    return this.http.get<Candidate[]>(`http://localhost:5273/api/Candidates`) 
  }
  getVM(): Observable<Candidate[]> {
    return this.http.get<Candidate[]>(`http://localhost:5273/api/Candidates/VM`)
  }
  getVMById(id: Number): Observable<Candidate> {
    return this.http.get<Candidate>(`http://localhost:5273/api/Candidates/VM/${id}`)
  }
  getQulifications(id: number): Observable<Qualification[]> {
    return this.http.get<Candidate[]>(`http://localhost:5273/api/Candidates/${id}/Qualifications`)
  }
  post(data: CandidateInputModel): Observable<Candidate> {
    return this.http.post<Candidate>(`http://localhost:5273/api/Candidates`, data)
  }
  put(data: CandidateInputModel): Observable<Candidate> {
    return this.http.put<Candidate>(`http://localhost:5273/api/Candidates/${data.candidateId}`, data)
  }
  delete(id: number): Observable<Candidate> {
    return this.http.delete<Candidate>(`http://localhost:5273/api/Candidates/${id}`)
  }
  uploadImage(id: number, f: File): Observable<ImagePathRespoonse> {
    const formData = new FormData();

    formData.append('picture', f);
    //console.log(f);
    return this.http.post<ImagePathRespoonse>(`http://localhost:5273/api/Candidates/Upload/${id}`, formData);
  }
}
