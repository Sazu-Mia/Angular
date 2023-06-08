import { Qualification } from "./qualification";

export interface Candidate {
  candidateId?: number;
  candidateName?: string
  birthDate?: Date | string;
  appliedFor?: string;
  expectedSalary?: number;
  workFromHome?: boolean
  picture?: string;
  qualifications?: Qualification[];
}
