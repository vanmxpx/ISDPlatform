import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileLayoutComponent } from './profile.layout';

describe('ProfileLayoutComponent', () => {
  let component: ProfileLayoutComponent;
  let fixture: ComponentFixture<ProfileLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
