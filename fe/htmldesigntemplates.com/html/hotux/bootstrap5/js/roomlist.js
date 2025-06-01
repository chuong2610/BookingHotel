// Hàm lưu thông tin đặt phòng vào localStorage
function saveBookingInfo() {
    // Lấy danh sách các room type đã chọn
    const selectedRoomTypes = [];
    // Tìm tất cả các checkbox đã được check trong phần room type
    $('.room-type input[type="checkbox"]:checked').each(function() {
        // Lấy text của label bên cạnh checkbox
        const roomType = $(this).closest('.pretty').find('label').text().trim();
        selectedRoomTypes.push(roomType);
    });

    // Lấy giá trị min và max price từ range slider
    const minPricePerNight = $('.range-slider-ui .min-value').text().replace('$', '').trim();
    const maxPricePerNight = $('.range-slider-ui .max-value').text().replace('$', '').trim();

    // Tạo object chứa thông tin booking
    const roomListInfo = {
        roomTypes: selectedRoomTypes,
        minPricePerNight,
        maxPricePerNight,
        timestamp: new Date().getTime()
    };

    // Lưu vào localStorage
    localStorage.setItem('roomListInfo', JSON.stringify(roomListInfo));
    // console.log('Saved booking info:', bookingInfo); // Debug log
}

// Hàm load thông tin đặt phòng từ localStorage
// function loadBookingInfo() {
//     // Lấy thông tin từ localStorage
//     const savedInfo = localStorage.getItem('bookingInfo');
//     if (savedInfo) {
//         const bookingInfo = JSON.parse(savedInfo);
//         console.log('Loading booking info:', bookingInfo); // Debug log
        
//         // Set room types
//         if (bookingInfo.roomTypes && bookingInfo.roomTypes.length > 0) {
//             // Duyệt qua tất cả các checkbox
//             $('.room-type input[type="checkbox"]').each(function() {
//                 // Lấy text của label
//                 const label = $(this).closest('.pretty').find('label').text().trim();
//                 // Nếu label nằm trong danh sách đã lưu thì check vào
//                 if (bookingInfo.roomTypes.includes(label)) {
//                     $(this).prop('checked', true);
//                 }
//             });
//         }

//         // Set price range
//         if (bookingInfo.minPricePerNight) {
//             $('.range-slider-ui .min-value').text(bookingInfo.minPricePerNight + ' $');
//         }
//         if (bookingInfo.maxPricePerNight) {
//             $('.range-slider-ui .max-value').text(bookingInfo.maxPricePerNight + ' $');
//         }
//     }
// }

// Đợi document ready
$(document).ready(function() {
    console.log('Document ready - Initializing roomlist.js'); // Debug log
    
    // Load thông tin đã lưu khi trang được tải
    // setTimeout(function() {
    //     loadBookingInfo();
    // }, 500);

    // Thêm event listener cho room type checkboxes
    $('.room-type input[type="checkbox"]').on('change', function() {
        console.log('Checkbox changed:', $(this).closest('.pretty').find('label').text().trim()); // Debug log
        saveBookingInfo();
    });

    // Thêm event listener cho price range slider
    $('.range-slider-ui').on('slidechange', function() {
        console.log('Price range changed'); // Debug log
        saveBookingInfo();
    });
}); 