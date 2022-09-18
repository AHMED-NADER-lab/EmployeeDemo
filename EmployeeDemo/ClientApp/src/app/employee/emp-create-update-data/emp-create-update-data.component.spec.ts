import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpCreateUpdateDataComponent } from './emp-create-update-data.component';

describe('EmpCreateUpdateDataComponent', () => {
  let component: EmpCreateUpdateDataComponent;
  let fixture: ComponentFixture<EmpCreateUpdateDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmpCreateUpdateDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmpCreateUpdateDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
