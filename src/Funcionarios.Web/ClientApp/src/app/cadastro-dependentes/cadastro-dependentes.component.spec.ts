import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroDependentesComponent } from './cadastro-dependentes.component';

describe('CadastroDependentesComponent', () => {
  let component: CadastroDependentesComponent;
  let fixture: ComponentFixture<CadastroDependentesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CadastroDependentesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroDependentesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
