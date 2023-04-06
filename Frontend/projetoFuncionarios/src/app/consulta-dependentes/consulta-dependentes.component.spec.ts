import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaDependentesComponent } from './consulta-dependentes.component';

describe('ConsultaDependentesComponent', () => {
  let component: ConsultaDependentesComponent;
  let fixture: ComponentFixture<ConsultaDependentesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsultaDependentesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultaDependentesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
