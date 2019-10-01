import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmPasswordFormComponent } from './confirm-password-form.component';

describe('LoginFormComponent', () => {
  let component: ConfirmPasswordFormComponent;
  let fixture: ComponentFixture<ConfirmPasswordFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ConfirmPasswordFormComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmPasswordFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
