// // Hàm lưu thông tin đặt phòng vào localStorage
// function saveBookingInfo() {
//     // Lấy danh sách các room type đã chọn
//     const selectedRoomTypes = [];
//     // Tìm tất cả các checkbox đã được check trong phần room type
//     $('.room-type input[type="checkbox"]:checked').each(function () {
//         // Lấy text của label bên cạnh checkbox
//         const roomType = $(this).closest('.pretty').find('label').text().trim();
//         selectedRoomTypes.push(roomType);
//     });

//     // Lấy giá trị min và max price từ range slider
//     const minPricePerNight = $('.range-slider-ui .min-value').text().replace('$', '').trim();
//     const maxPricePerNight = $('.range-slider-ui .max-value').text().replace('$', '').trim();

//     // Tạo object chứa thông tin booking
//     const bookingInfo = {
//         roomTypes: selectedRoomTypes,
//         minPricePerNight,
//         maxPricePerNight,
//         timestamp: new Date().getTime()
//     };

//     // Lưu vào localStorage
//     localStorage.setItem('bookingInfo', JSON.stringify(bookingInfo));
// }

// Hàm gọi API lấy danh sách phòng
async function fetchRooms() {
    try {
        // Lấy thông tin từ localStorage
        const savedInfo = localStorage.getItem('bookingInfo');
        let requestBody = {
            codes: [],
            minPricePerNight: 0,
            maxPricePerNight: 2000
        };

        if (savedInfo) {
            const bookingInfo = JSON.parse(savedInfo);
            requestBody.codes = bookingInfo.roomTypes;
            requestBody.minPricePerNight = parseInt(bookingInfo.minPricePerNight) || 0;
            requestBody.maxPricePerNight = parseInt(bookingInfo.maxPricePerNight) || 2000;
            requestBody.checkIn = bookingInfo.checkIn || null;
            requestBody.checkOut = bookingInfo.checkOut || null;
            requestBody.adults = bookingInfo.adults || 1;
            requestBody.children = bookingInfo.children || 0;
        }

        // Gọi API
        const response = await fetch('http://localhost:5178/api/RoomType/available', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const result = await response.json();
        const rooms = result.data || [];
        displayRooms(rooms);
    } catch (error) {
        console.error('Error fetching rooms:', error);
    }
}

// Hàm hiển thị danh sách phòng
function displayRooms(rooms) {
    const roomListContainer = $('.list-grid');
    roomListContainer.empty(); // Xóa nội dung cũ

    rooms.forEach(room => {
        const roomHtml = `
            <div class="room-grid">
                <div class="grid-image">
                    <img src="${room.img}" alt="${room.name}" />
                </div>
                <div class="grid-content">
                    <div class="room-title">
                        <h4 room-id="${room.id}">${room.name}</h4>
                        <p class="mar-top-5"><i class="fa fa-tag"></i> $${room.pricePerNight}/Night</p>
                        <div class="deal-rating">
                            ${generateRatingStars(room.rating)}
                        </div>
                    </div>
                    <div class="room-detail">
                        <p>${room.summaryDescription}</p>
                    </div>
                    <div class="room-services">
                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <i class="fa fa-bed" aria-hidden="true"></i> Max Occupancy: ${room.maxOccupancy}
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <i class="fa fa-child" aria-hidden="true"></i> Children Allowed: ${room.childrenAllowed}
                            </div>
                        </div>
                    </div>
                    <div class="grid-btn mar-top-20">
                        <a href="#" class="btn btn-black mar-right-10">VIEW DETAILS</a>
                        <a href="#" class="btn btn-orange">BOOK NOW</a>
                    </div>
                </div>
            </div>
        `;
        roomListContainer.append(roomHtml);
    });
}

// Hàm tạo HTML cho rating stars
function generateRatingStars(rating) {
    let starsHtml = '';
    for (let i = 1; i <= 5; i++) {
        if (i <= rating) {
            starsHtml += '<span class="fa fa-star checked"></span>';
        } else {
            starsHtml += '<span class="fa fa-star"></span>';
        }
    }
    return starsHtml;
}

// Đợi document ready
$(document).ready(function () {
    // Load danh sách phòng khi trang được tải
    fetchRooms();

    // Thêm event listener cho room type checkboxes
    $('.room-type input[type="checkbox"]').on('change', function () {
        saveBookingInfo();
        fetchRooms(); // Cập nhật lại danh sách phòng khi thay đổi filter
    });

    // Thêm event listener cho price range slider
    $('.range-slider-ui').on('slidechange', function () {
        saveBookingInfo();
        fetchRooms(); // Cập nhật lại danh sách phòng khi thay đổi filter
    });

    // Hàm xử lý click vào nút Book Now
    $(document).on('click', '.btn-orange', function(e) {
        e.preventDefault();
        const roomId = $(this).closest('.room-grid').find('.room-title h4').attr('room-id');
        if (roomId) {
            localStorage.setItem('selectedRoomId', roomId);
            window.location.href = 'http://127.0.0.1:5501/fe/htmldesigntemplates.com/html/hotux/bootstrap5/booking.html';
        }
    });

    // Hàm xử lý click vào nút View Details
    $(document).on('click', '.btn-black', function(e) {
        e.preventDefault();
        const roomId = $(this).closest('.room-grid').find('.room-title h4').attr('room-id');
        if (roomId) {
            localStorage.setItem('selectedRoomId', roomId);
            window.location.href = 'http://127.0.0.1:5501/fe/htmldesigntemplates.com/html/hotux/bootstrap5/booking.html';

        }
    });
});

