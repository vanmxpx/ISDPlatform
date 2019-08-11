import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserConnectionsListComponent } from './user-connections-list.component';

describe('UserConnectionsListComponent', () => {
  let component: UserConnectionsListComponent;
  let fixture: ComponentFixture<UserConnectionsListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserConnectionsListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserConnectionsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
