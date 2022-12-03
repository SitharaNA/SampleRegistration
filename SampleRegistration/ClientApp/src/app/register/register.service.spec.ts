import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RegisterService } from './register.service';

describe('RegisterService', () => {
  let service: RegisterService;
  let httpTestingController: HttpTestingController;
  let url: string = 'http://localhost:9002/';
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [RegisterService,
        { provide: 'BASE_URL', useValue: url }]
    });
    service = TestBed.inject(RegisterService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should save userdata', () => {
    const formData: FormData = new FormData();
    service.saveUser(formData).subscribe();
    const req = httpTestingController.expectOne('http://localhost:9002/api/registration/save');
    expect(req.request.method).toEqual('POST');
    req.flush(formData);
  });
});
