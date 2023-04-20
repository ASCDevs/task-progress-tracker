import { TestBed } from '@angular/core/testing';

import { TasksSavedService } from './tasks-saved.service';

describe('TasksSavedService', () => {
  let service: TasksSavedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TasksSavedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
