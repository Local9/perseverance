export interface ICitizen {
  id: string;
  name: string;
  surname: string;
  imageId: string;
  imageBlurData: string;
  socialSecurityNumber: string;
  fullname: string = `${this.name} ${this.surname}`;
}