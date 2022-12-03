import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';

import { RegisterComponent } from './register.component';
import { RegisterService } from './register.service';

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let service: jasmine.SpyObj<RegisterService>;

  beforeEach(async () => {
    service = jasmine.createSpyObj('RegisterService', { 'saveUser': of(true)});
    await TestBed.configureTestingModule({
      declarations: [RegisterComponent],
      schemas: [NO_ERRORS_SCHEMA],
      providers: [RegisterService, { provide: RegisterService, useValue: service }]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    service = TestBed.inject(RegisterService) as jasmine.SpyObj<RegisterService>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('form invalid when empty', () => {
    expect(component.form.valid).toBeFalsy();
  });

  it('password field validity', () => {
    let password = component.form.controls['password']; 
    expect(password.valid).toBeFalsy(); 
  });

  it('password field validity', () => {
    let password = component.form.controls['password'];
    let errors = password.errors || {};
    expect(errors['required']).toBeTruthy();
  });

  it('submitting a form saves user', () => {
    expect(component.form.valid).toBeFalsy();
    let obj = {
      firstName: "sithara",
      lastName: "aiyappa"
    };
    var name = component.form.controls['name'] as FormGroup;
    name.patchValue(obj);
    component.form.controls['email'].setValue("test@gmail.com");
    component.form.controls['password'].setValue("67849989876435900000");
    expect(component.form.valid).toBeTruthy();
    component.onSubmit();
    expect(service.saveUser).toHaveBeenCalledTimes(1);
  });

});

