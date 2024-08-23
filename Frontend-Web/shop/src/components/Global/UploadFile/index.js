import { useEffect, useRef } from 'react';

function UploadFile() {
    const cloudinaryRef = useRef();
    const widgetRef = useRef();
    useEffect(() => {
        cloudinaryRef.current = window.cloudinary;
        widgetRef.current = cloudinaryRef.current.createUploadWidget(
            {
                cloudName: 'drtmlglka',
                uploadPreset: 'uxttfjg4',
            },
            function (error, result) {
                console.log(result);
            },
        );
    }, []);

    return <button onClick={() => widgetRef.current.open()}>upload</button>;
}

export default UploadFile;
