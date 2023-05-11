import type { ICitizen } from "../interface/citizen";

export class Citizen implements ICitizen {
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

  constructor(data: {
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
  }) {
    this.id = data.id;
    this.name = data.name;
    this.surname = data.surname;
    this.imageId = data.imageId;
    this.imageBlurData = data.imageBlurData;
    this.socialSecurityNumber = data.socialSecurityNumber;
    this.dateOfBirth = data.dateOfBirth;
    this.gender = data.gender;
    this.hairColor = data.hairColor;
    this.eyeColor = data.eyeColor;
    this.height = data.height;
    this.weight = data.weight;
    this.address = data.address;
    this.postal = data.postal;
    this.ethnicity = data.ethnicity;
  }

  get fullname() {
    return `${this.name} ${this.surname}`;
  }
}
