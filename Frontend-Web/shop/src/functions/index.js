import axios from 'axios';

export const NumberVND = (x) => {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.');
};

export const getWard = async (code) => {
    return await axios
        .get(`https://provinces.open-api.vn/api/w/31299`)
        .then((res) => {
            if (res.status === 200) {
                return res.data;
            }
        })
        .catch((err) => {
            console.log(err);
        });
};

export const getDistrict = (code) => {
    axios
        .get(`https://provinces.open-api.vn/api/d/${code}`)
        .then((res) => {
            if (res.status === 200) {
                return res.data;
            }
        })
        .catch((err) => {
            console.log(err);
            return null;
        });
};

export const getProvince = (code) => {
    axios
        .get(`https://provinces.open-api.vn/api/p/${code}`)
        .then((res) => {
            if (res.status === 200) {
                return res.data;
            }
        })
        .catch((err) => {
            console.log(err);
            return null;
        });
};
