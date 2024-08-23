import React from 'react';
import { useState } from 'react';

const initUrl = '';

function FileReview(props) {
  const {onChange,value, } = props
  const [url, setUrl] = useState(initUrl);

  const showReview = (e) => {
    if (e.target.files && e.target.files[0]) {
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (x) => {
        onChange(e)
        setUrl(x.target.result)
      };
      reader.readAsDataURL(imageFile);
    } else {
      setUrl(initUrl);
    }
  };
  return (
    <>
      <div className="file-review">
        <input
          type={'file'}
          accept="image/*,video/*"
          onChange={showReview}
          id="field-upload"
        />
        <img src={url} alt="" className="img-review" />
      </div>
    </>
  );
}

export default FileReview;
