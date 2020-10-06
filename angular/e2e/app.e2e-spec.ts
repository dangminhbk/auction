import { WebShopTemplatePage } from './app.po';

describe('WebShop App', function() {
  let page: WebShopTemplatePage;

  beforeEach(() => {
    page = new WebShopTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
