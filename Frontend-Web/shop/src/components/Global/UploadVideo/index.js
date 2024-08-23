import classNames from 'classnames/bind';
import styles from './UploadVideo.module.scss';
import { PlusOutlined, UploadOutlined } from '@ant-design/icons';
import { Modal, Upload, message, Button } from 'antd';
import { useState } from 'react';
import axios from 'axios';

const cx = classNames.bind(styles);

function UploadVideo({ listVideo, setListVideo }) {
    const uploadVideo = (file) => {
        const data = new FormData();
        data.append('file', file);
        data.append('upload_preset', 'qbkvgvkb');
        data.append('cloud_name', 'drtmlglka');
        axios
            .post(`https://api.cloudinary.com/v1_1/drtmlglka/video/upload`, data)
            .then((res) => {
                const newVideo = {
                    id: 0,
                    url: res.data.url,
                    isCover: false,
                    isVideo: true,
                    isDeleted: false,
                    isNewlyAdded: false,
                };
                setListVideo((prev) => [...prev, newVideo]);
                console.log(res);
            })
            .catch((err) => console.log(err));
    };

    const uploadButton = (
        <div>
            <PlusOutlined />
            <div
                style={{
                    marginTop: 8,
                }}
            >
                Upload
            </div>
        </div>
    );
    return (
        <div className={cx('wrap')}>
            <Upload
                listType="picture-card"
                fileList={listVideo}
                beforeUpload={() => false}
                onChange={(e) => uploadVideo(e.file)}
            >
                {listVideo.length >= 3 ? null : uploadButton}
            </Upload>
        </div>
    );
}

export default UploadVideo;
