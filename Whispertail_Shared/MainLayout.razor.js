export function isDevice() {
    return /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini|mobile/i.test(navigator.userAgent);
}

export function isDarkMode() {
    let matched = window.matchMedia("(prefers-color-scheme: dark)").matches;

    if (matched)
        return true;
    else
        return false;
}



export function themeChangeInitialize(dotNetHelper) {
    window
        .matchMedia("(prefers-color-scheme: dark)")
        .addEventListener("change", function (e) {
            dotNetHelper.invokeMethodAsync("UpdateTheme");
        });
}