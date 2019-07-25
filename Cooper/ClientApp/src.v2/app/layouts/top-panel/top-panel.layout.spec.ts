import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TopPanelLayoutComponent } from './top-panel.layout';

describe('TopPanelLayoutComponent', () => {
  let component: TopPanelLayoutComponent;
  let fixture: ComponentFixture<TopPanelLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TopPanelLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopPanelLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
