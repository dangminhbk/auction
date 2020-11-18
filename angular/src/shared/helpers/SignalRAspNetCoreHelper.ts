import { AppConsts } from '@shared/AppConsts';
import { UtilsService } from 'abp-ng2-module';

export class SignalRAspNetCoreHelper {
    static initSignalR(callback?: () => void): void {
        const encryptedAuthToken = new UtilsService().getCookieValue(AppConsts.authorization.encryptedAuthTokenName);

        let qs = '';
        if (encryptedAuthToken != null) {
            qs = AppConsts.authorization.encryptedAuthTokenName + '=' + encodeURIComponent(encryptedAuthToken)
        }

        abp.signalr = {
            autoConnect: true,
            connect: undefined,
            hubs: undefined,
            qs: qs,
            remoteServiceBaseUrl: AppConsts.remoteServiceBaseUrl,
            startConnection: undefined,
            url: '/signalr'
        };

        const script = document.createElement('script');
        if (callback) {
            script.onload = () => {
                callback();
            };
        }
        script.src = AppConsts.appBaseUrl + '/assets/abp/abp.signalr-client.js';
        document.head.appendChild(script);
    }
}
