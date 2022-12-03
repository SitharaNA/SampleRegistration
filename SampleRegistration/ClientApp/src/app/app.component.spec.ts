import { NO_ERRORS_SCHEMA } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { CardModule } from 'primeng/card';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
      ],
      declarations: [
        AppComponent
      ],
      schemas: [NO_ERRORS_SCHEMA],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'SampleRegistration'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('SampleRegistration');
  });

  it('renders app register', () => {
    const { debugElement } = TestBed.createComponent(AppComponent);
    const counter = debugElement.query(By.css('app-register'));
    expect(counter).toBeTruthy();
  });
});
