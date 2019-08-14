import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResetPasswordLayoutComponent } from './reset-password.layout';

describe('LoginComponent', () => {
  let component: ResetPasswordLayoutComponent;
  let fixture: ComponentFixture<ResetPasswordLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResetPasswordLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetPasswordLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
