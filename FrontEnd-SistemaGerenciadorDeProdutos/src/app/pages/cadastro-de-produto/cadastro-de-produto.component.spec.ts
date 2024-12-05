import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroDeProdutoComponent } from './cadastro-de-produto.component';

describe('CadastroDeProdutoComponent', () => {
  let component: CadastroDeProdutoComponent;
  let fixture: ComponentFixture<CadastroDeProdutoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroDeProdutoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CadastroDeProdutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
