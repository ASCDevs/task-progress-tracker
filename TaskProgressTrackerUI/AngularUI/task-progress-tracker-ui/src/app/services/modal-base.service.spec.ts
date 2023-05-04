import { TestBed } from '@angular/core/testing';

import { ModalBaseService } from './modal-base.service';

describe('ModalBaseService', () => {
  let service: ModalBaseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModalBaseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
