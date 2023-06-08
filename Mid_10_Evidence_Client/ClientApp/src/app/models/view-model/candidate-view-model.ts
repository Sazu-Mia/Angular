import { Qualification } from "../data/qualification";

export interface CandidateViewModel {
  candidateId?: number;
  candidateName?: string
  birthDate?: Date | string;
  appliedFor?: string;
  expectedSalary?: number;
  workFromHome?: boolean
  picture?: string;
  qualifactionCount?: number;
  canDelete?: boolean;
  qulification?: Qualification[];
}
