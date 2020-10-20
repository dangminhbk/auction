import { AccountServiceProxy, RegisterInput, RegisterOutput } from '../../../shared/service-proxies/service-proxies';

export class RegisterSellerDto {
  description: string;
  sellerName: string;
  address: string;
  phoneNumber: string;

  name: string | undefined;
  surname: string | undefined;
  userName: string | undefined;
  emailAddress: string | undefined;
  password: string | undefined;
  captchaResponse: string | undefined;
}