import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalChatsLayoutComponent } from './personal-chats.layout';

describe('PersonalChatsLayoutComponent', () => {
  let component: PersonalChatsLayoutComponent;
  let fixture: ComponentFixture<PersonalChatsLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PersonalChatsLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PersonalChatsLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
