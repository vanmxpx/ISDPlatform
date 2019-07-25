import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageNotFoundLayoutComponent } from './page-not-found.layout';

describe('PageNotFoundLayoutComponent', () => {
  let component: PageNotFoundLayoutComponent;
  let fixture: ComponentFixture<PageNotFoundLayoutComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PageNotFoundLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageNotFoundLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
