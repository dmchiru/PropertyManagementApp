window.getLocation = () => {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation) {
            reject("Geolocation not supported");
            return;
        }

        navigator.geolocation.getCurrentPosition(
            position => {
                const lat = position.coords.latitude;
                const lng = position.coords.longitude;
                resolve(`${lat},${lng}`);
            },
            error => {
                reject(error.message);
            }
        );
    });
};