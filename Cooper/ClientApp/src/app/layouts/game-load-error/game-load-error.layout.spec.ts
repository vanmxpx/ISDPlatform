import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GameLoadErrorLayoutComponent } from './game-load-error.layout';

describe('GameLoadErrorLayoutComponent', () => {
  let component: GameLoadErrorLayoutComponent;
  let fixture: ComponentFixture<GameLoadErrorLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GameLoadErrorLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GameLoadErrorLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
