import { TestBed } from '@angular/core/testing';

import { UsuarioAPIService } from './usuario-api.service';

describe('UsuarioAPIService', () => {
  let service: UsuarioAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UsuarioAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
