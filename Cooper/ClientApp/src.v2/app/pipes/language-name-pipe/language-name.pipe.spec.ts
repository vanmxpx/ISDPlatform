import { LanguageNamePipe } from '@pipes';

describe('LanguageNamePipe', () => {
  it('create an instance', () => {
    const pipe = new LanguageNamePipe();
    expect(pipe).toBeTruthy();
  });
});
