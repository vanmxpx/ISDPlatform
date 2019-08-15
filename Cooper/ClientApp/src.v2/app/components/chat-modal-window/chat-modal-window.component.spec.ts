import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatModalWindowComponent } from './chat-modal-window.component';

describe('ChatModalWindowComponent', () => {
  let component: ChatModalWindowComponent;
  let fixture: ComponentFixture<ChatModalWindowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChatModalWindowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChatModalWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
