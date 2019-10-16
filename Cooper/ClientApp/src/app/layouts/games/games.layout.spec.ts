import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GamesLayoutComponent } from './games.layout';

describe('GamesLayoutComponent', () => {
  let component: GamesLayoutComponent;
  let fixture: ComponentFixture<GamesLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GamesLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GamesLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
