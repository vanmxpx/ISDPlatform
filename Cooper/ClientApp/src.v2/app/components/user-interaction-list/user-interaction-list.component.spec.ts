import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserInteractionListComponent } from './user-interaction-list.component';

describe('UserInteractionListComponent', () => {
  let component: UserInteractionListComponent;
  let fixture: ComponentFixture<UserInteractionListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserInteractionListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserInteractionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
