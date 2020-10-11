import {AppConsts} from '@shared/AppConsts';
export class UrlHelper {
    /**
     * The URL requested, before initial routing.
     */
    static readonly initialUrl = location.href;

    static getQueryParameters(): any {
        return document.location.search
            .replace(/(^\?)/, '')
            .split('&')
            .map(function (n) { return n = n.split('='), this[n[0]] = n[1], this; }.bind({}))[0];
    }

    static getImagePath(url: string): string {
        return `${AppConsts.remoteServiceBaseUrl}\\${url}`;
    }
}
