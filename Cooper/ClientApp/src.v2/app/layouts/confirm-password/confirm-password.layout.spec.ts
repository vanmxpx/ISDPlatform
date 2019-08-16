import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmPasswordLayoutComponent } from './confirm-password.layout';

describe('LoginComponent', () => {
  let component: ConfirmPasswordLayoutComponent;
  let fixture: ComponentFixture<ConfirmPasswordLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmPasswordLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmPasswordLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
