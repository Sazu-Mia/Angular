import { Candidate } from "./candidate";

export interface Qualification {
  qualificationId?: number
  degree?: string;
  passingYear?: number;
  institute?: string;
  result?: string;
  candidateId?: number;
  candidate?: Candidate;
}
