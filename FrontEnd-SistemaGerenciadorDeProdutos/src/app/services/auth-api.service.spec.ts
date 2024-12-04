import { TestBed } from '@angular/core/testing';

import { AuthAPIService } from './auth-api.service';

describe('AuthAPIService', () => {
  let service: AuthAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
