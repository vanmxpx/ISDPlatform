import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EntryLayoutComponent } from './entry.layout';

describe('EntryLayoutComponent', () => {
  let component: EntryLayoutComponent;
  let fixture: ComponentFixture<EntryLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EntryLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EntryLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
