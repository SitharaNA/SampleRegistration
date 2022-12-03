import {
  Component,
} from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators
} from '@angular/forms';
import { RegisterService } from './register.service';
import * as bcrypt from 'bcryptjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent {
  form: any;
  firstName: any;
  lastName: any;
  email: any;
  password: any;

  constructor(private service: RegisterService)
{
    this.getControls();
    this.getForm();
}

  getControls() {
    this.firstName = new FormControl("", Validators.required);
    this.lastName = new FormControl("", Validators.required);
    this.email = new FormControl("", [
      Validators.required,
      Validators.pattern("^[A-Za-z0-9+_.-]+@(.+)$")
    ]);
    this.password = new FormControl("", [
      Validators.required,
      Validators.minLength(10)
    ]);
  }

  getForm() {
    this.form = new FormGroup({
      name: new FormGroup({
        firstName: this.firstName,
        lastName: this.lastName
      }),
      email: this.email,
      password: this.password,
    });
  }

  onSubmit() {
    if (this.form.valid) {
      let salt = bcrypt.genSaltSync(10);
      this.form.value.password = bcrypt.hashSync(this.form.value.password, salt);
      this.service.saveUser(this.form.value).subscribe();
      this.form.reset();
    }
  }
}
