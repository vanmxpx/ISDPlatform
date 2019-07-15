import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlatformLayoutComponent } from './platform.layout';

describe('PlatformLayoutComponent', () => {
  let component: PlatformLayoutComponent;
  let fixture: ComponentFixture<PlatformLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlatformLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlatformLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
