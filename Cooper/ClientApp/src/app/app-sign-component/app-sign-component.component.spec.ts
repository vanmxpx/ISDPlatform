import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AppSignComponentComponent } from './app-sign-component.component';

describe('AppSignComponentComponent', () => {
  let component: AppSignComponentComponent;
  let fixture: ComponentFixture<AppSignComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AppSignComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppSignComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
