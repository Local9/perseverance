export interface ICitizen {
  id: string;
  name: string;
  surname: string;
  imageId: string;
  imageBlurData: string;
  socialSecurityNumber: string;
  dateOfBirth: Date;
  gender: string;
  hairColor: string;
  eyeColor: string;
  height: number;
  weight: number;
  address: string;
  postal: string;
  ethnicity: string;
  get fullname(): string;
}
