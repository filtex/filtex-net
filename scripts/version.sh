#!/bin/bash

new_version() {
    LAST_TAG=$(git tag --sort "committerdate" | tail -n1)

    if [ "$LAST_TAG" == "" ]; then
        NEW_VERSION="1.0.0"
    else
        DIFF_COMMITS=$(git log $LAST_TAG..HEAD --pretty=format:"%s")

        if [ "$DIFF_COMMITS" == "" ]; then
            echo "Could not found any commit"
            exit 1
        else
            VERSION_LEVEL="patch"

            while IFS=\n read -r m; do
                COMMIT_TYPE=""

                MESSAGE_PATTERN='^([a-z]+)?(\(([a-z]+)\))?(!)?: (.*)'
                
                if [[ $m =~ $MESSAGE_PATTERN ]]; then
                    COMMIT_TYPE="${BASH_REMATCH[1]}"
                    COMMIT_SCOPE="${BASH_REMATCH[3]}"
                    COMMIT_BREAKING="${BASH_REMATCH[4]}"
                    COMMIT_MESSAGE="${BASH_REMATCH[5]}"
                fi

                case $COMMIT_TYPE in
                "feat")
                    COMMIT_LEVEL="minor"
                    ;;

                "fix")
                    COMMIT_LEVEL="patch"
                    ;;

                *)
                    COMMIT_LEVEL="patch"
                    ;;
                esac

                if [[ $COMMIT_BREAKING == '!' ]]; then
                    COMMIT_LEVEL="major"
                fi

                if [[ $COMMIT_LEVEL == 'major' ]]; then
                    VERSION_LEVEL="major"
                fi

                if [[ $COMMIT_LEVEL == 'minor' ]]; then
                    if [[ $VERSION_LEVEL == 'patch' ]]; then
                        VERSION_LEVEL="minor"
                    fi
                fi
            done <<< "$DIFF_COMMITS"

            VERSION_PARTS=(${LAST_TAG//./ })

            MAJOR_PART=${VERSION_PARTS[0]//[^0-9]/}
            MINOR_PART=${VERSION_PARTS[1]//[^[0-9]/}
            PATCH_PART=${VERSION_PARTS[2]//[^0-9]/}

            if [ "$VERSION_LEVEL" == "major" ]; then
                MAJOR_PART=$((MAJOR_PART+1))
                MINOR_PART=0
                PATCH_PART=0
            elif [ "$VERSION_LEVEL" == "minor" ]; then
                MINOR_PART=$((MINOR_PART+1))
                PATCH_PART=0
            else
                PATCH_PART=$((PATCH_PART+1))
            fi

            NEW_VERSION="$MAJOR_PART.$MINOR_PART.$PATCH_PART"
        fi
    fi
}

rm -f version.txt
new_version
echo "New Version: ${NEW_VERSION}"
echo "$NEW_VERSION" > version.txt