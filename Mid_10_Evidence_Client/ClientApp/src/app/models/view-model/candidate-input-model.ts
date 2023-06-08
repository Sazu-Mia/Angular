import { Qualification } from "../data/qualification";

export interface CandidateInputModel {
  candidateId?: number;
  candidateName?: string
  birthDate?: Date | string;
  appliedFor?: string;
  expectedSalary?: number;
  workFromHome?: boolean
  picture?: string;
  qualifications?: Qualification[];
}
