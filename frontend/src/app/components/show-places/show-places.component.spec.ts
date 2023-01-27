import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowPlacesComponent } from './show-places.component';

describe('ShowPlacesComponent', () => {
  let component: ShowPlacesComponent;
  let fixture: ComponentFixture<ShowPlacesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowPlacesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowPlacesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
