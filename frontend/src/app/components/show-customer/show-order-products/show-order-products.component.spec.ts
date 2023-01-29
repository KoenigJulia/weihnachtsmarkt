import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowOrderProductsComponent } from './show-order-products.component';

describe('ShowOrderProductsComponent', () => {
  let component: ShowOrderProductsComponent;
  let fixture: ComponentFixture<ShowOrderProductsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowOrderProductsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowOrderProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
