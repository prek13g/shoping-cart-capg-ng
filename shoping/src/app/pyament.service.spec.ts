import { TestBed } from '@angular/core/testing';

import { PyamentService } from './pyament.service';

describe('PyamentService', () => {
  let service: PyamentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PyamentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
