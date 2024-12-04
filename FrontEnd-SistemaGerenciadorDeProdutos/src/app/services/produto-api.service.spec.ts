import { TestBed } from '@angular/core/testing';

import { ProdutoAPIService } from './produto-api.service';

describe('ProdutoAPIService', () => {
  let service: ProdutoAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProdutoAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
