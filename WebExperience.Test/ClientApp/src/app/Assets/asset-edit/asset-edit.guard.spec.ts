import { TestBed } from '@angular/core/testing';

import { AssetEditGuard } from './asset-edit.guard';

describe('AssetEditGuard', () => {
  let guard: AssetEditGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AssetEditGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
