import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonitoramentoGeralComponent } from './monitoramento-geral.component';

describe('MonitoramentoGeralComponent', () => {
  let component: MonitoramentoGeralComponent;
  let fixture: ComponentFixture<MonitoramentoGeralComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MonitoramentoGeralComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MonitoramentoGeralComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
